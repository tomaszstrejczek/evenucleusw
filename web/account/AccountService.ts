    export interface IAccountService
    {
        Login(user: string, password: string): Promise<string>;
    };

    export class AccountService implements IAccountService {
        Login(user: string, password: string): Promise<string>
        {
            return new Promise<string>((resolve, reject) => {
                if (user == "admin" && password == "admin123")
                    resolve("ok1");
                else
                    reject("Invalid user or password");
            });
        }
    };

