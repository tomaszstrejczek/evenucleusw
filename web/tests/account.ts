import {AccountService}  from "../account/AccountService";

describe("Sanity check", () => {
    var a;

    it("Sanity check", () => {
        a = true;
        expect(a).toBe(true);
    });
});

describe("Authentication check", () => {
    var a = new AccountService();

    it("Valid password", (done) => {

        a.Login("admin", "admin123").then((token) => {
            expect(token).not.toBeNull();
        }).then(done);
    });

    it("Invalid password", (done) => {

        a.Login("admin", "admin").then((token) => {
            fail();
        }, (err) => {
            expect(err).toEqual("Invalid user or password");
        }).then(done);
    });
});