import { User } from './user.model';

export class Link {
  constructor(
    public id: number,
    public userId: number,
    public user: User,
    public name: string,
    public description: string,
    public shortUrl: string,
    public originalUrl: string,
    public isActive: boolean,
    public createdDate: Date,
    public updatedDate: Date
  ) {}
}
