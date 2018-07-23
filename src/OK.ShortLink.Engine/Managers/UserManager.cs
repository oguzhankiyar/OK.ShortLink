using OK.ShortLink.Common.Entities;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.Core.Managers;
using OK.ShortLink.Core.Mapping;
using OK.ShortLink.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OK.ShortLink.Engine.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, ILogger logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<UserModel> GetUsers(int pageSize = 15, int pageNumber = 1)
        {
            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;

            List<UserEntity> users = _userRepository.FindAll()
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToList();

            return _mapper.MapList<UserEntity, UserModel>(users);
        }

        public UserModel GetUserById(int id)
        {
            UserEntity user = _userRepository.FindOne(x => x.Id == id);

            return _mapper.Map<UserEntity, UserModel>(user);
        }

        public UserModel LoginUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            string hashedPassword = HashPasswordWithMD5(password);

            UserEntity user = _userRepository.FindOne(x => x.Username == username && x.Password == hashedPassword && x.IsActive == true);

            return _mapper.Map<UserEntity, UserModel>(user);
        }

        public UserModel CreateUser(int userId, string username, string password, bool isActive)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            UserEntity user = new UserEntity();

            user.Username = username;
            user.Password = HashPasswordWithMD5(password);
            user.IsActive = isActive;

            user = _userRepository.Insert(user);

            if (user.Id > 0)
            {
                _logger.DebugData(string.Join("/", nameof(UserManager), nameof(CreateUser)), "An user is created.", new { CreatedId = user.Id, CreatedBy = userId });
            }

            return _mapper.Map<UserEntity, UserModel>(user);
        }

        public bool EditUser(int userId, int id, string password, bool isActive)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            UserEntity user = _userRepository.FindOne(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Password = HashPasswordWithMD5(password);
            user.IsActive = isActive;

            bool isEdited = _userRepository.Update(user);

            if (isEdited)
            {
                _logger.DebugData(string.Join("/", nameof(UserManager), nameof(EditUser)), "An user is edited.", new { EditedId = id, EditedBy = userId });
            }

            return isEdited;
        }

        public bool EditUserPassword(int userId, int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            UserEntity user = _userRepository.FindOne(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Password = HashPasswordWithMD5(password);

            bool isEdited = _userRepository.Update(user);

            if (isEdited)
            {
                _logger.DebugData(string.Join("/", nameof(UserManager), nameof(EditUserPassword)), "An user is edited.", new { EditedId = id, EditedBy = userId });
            }

            return isEdited;
        }

        public bool EditUserActivation(int userId, int id, bool isActive)
        {
            UserEntity user = _userRepository.FindOne(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.IsActive = isActive;

            bool isEdited = _userRepository.Update(user);

            if (isEdited)
            {
                _logger.DebugData(string.Join("/", nameof(UserManager), nameof(EditUserActivation)), "An user is edited.", new { EditedId = id, EditedBy = userId });
            }

            return isEdited;
        }

        public bool DeleteUser(int userId, int id)
        {
            UserEntity user = _userRepository.FindOne(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            bool isDeleted = _userRepository.Remove(id);

            if (isDeleted)
            {
                _logger.DebugData(string.Join("/", nameof(UserManager), nameof(DeleteUser)), "An user is deleted.", new { DeletedId = id, DeletedBy = userId });
            }

            return isDeleted;
        }

        #region Helpers

        private string HashPasswordWithMD5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        #endregion
    }
}