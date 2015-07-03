export interface IAccountService
{
    Login(user: string, password: string): Promise<string>;
} 

export class AccountService implements IAccountService {
    Login(user: string, password: string): Promise<string>
    {
        return new Promise<string>((resolve, reject) => {
            resolve("ok");
        });
    }
}