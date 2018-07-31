export class StorageService {
  public get<T>(key: string): T {
    return JSON.parse(localStorage.getItem(key));
  }

  public set<T>(key: string, value: T): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  public del(key: string) {
    localStorage.removeItem(key);
  }
}
