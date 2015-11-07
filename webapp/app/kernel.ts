import {Kernel} from './../node_modules/inversify/source/inversify';

export class KernelCreator {
    public static create(): Kernel {
        var kernel = new Kernel();

        console.log('kernel created');
        return kernel;
    }
}