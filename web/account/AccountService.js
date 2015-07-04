;
export class AccountService {
    Login(user, password) {
        return new Promise((resolve, reject) => {
            if (user == "admin" && password == "admin123")
                resolve("ok1");
            else
                reject("Invalid user or password");
        });
    }
}
;
//# sourceMappingURL=AccountService.js.map