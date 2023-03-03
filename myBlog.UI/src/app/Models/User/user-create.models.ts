export class UserCreate{

    constructor(
       public username: string,
       public password:string,
       public email:string,
       public fullname?:string
    ){}
}