import {Kernel, TypeBinding} from './../node_modules/inversify/source/inversify';
import {IAuthService, AuthService} from './../api/AuthService';
import * as Restful from 'restful.js';


export class KernelCreator {
    public static create(): Kernel {
        var kernel = new Kernel();
        kernel.bind(new TypeBinding<IAuthService>("IAuthService", AuthService));
        //kernel.bind(new TypeBinding<Restful.Api>("Api"), Restful.default("http://localhost:8080"));

        console.log('kernel created');
        return kernel;
    }
}