import {Kernel, TypeBinding} from './../node_modules/inversify/source/inversify';
import {IAuthService, AuthService} from './../api/AuthService';


export class KernelCreator {
    public static create(): Kernel {
        var kernel = new Kernel();
        kernel.bind(new TypeBinding<IAuthService>("IAuthService", AuthService));

        console.log('kernel created');
        return kernel;
    }
}