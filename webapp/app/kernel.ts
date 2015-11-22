import * as TypeIoc from 'typeioc';
import {IAuthService, AuthService} from './../api/AuthService';
import * as Restful from 'restful.js';


export class KernelCreator {
    public static create(): TypeIoc.IContainer {
        var kernel = TypeIoc.createBuilder();
        kernel.register<IAuthService>(AuthService).as(() => new AuthService());

        var api = Restful.default("http://localhost:8080");
        kernel.register<Restful.Api>(api).as(() => api).within(TypeIoc.Types.Scope.Container);

        console.log('kernel created');
        return kernel.build();
    }
}