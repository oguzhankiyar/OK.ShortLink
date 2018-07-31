import { Link } from './link.model';

export class Visitor {
  constructor(
    public id: number,
    public linkId: number,
    public link: Link,
    public ipAddress: string,
    public userAgent: string,
    public osInfo: string,
    public deviceInfo: string,
    public browserInfo: string,
    public createdDate: Date,
    public updatedDate: Date
  ) {}
}
