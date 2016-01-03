import * as TypeIoc from 'typeioc';
import {IAuthService, AuthService} from './../api/AuthService';
import {IKeyInfoService, KeyInfoService} from './../api/KeyInfoService';
import {IApiCaller} from './../api/IApiCaller';
import {ApiCaller} from './../api/ApiCaller';
import {IDeferredActionExecutor, DeferredActionExecutor} from './../utils/DeferredActionExecutor';
import {IBackgroundUpdateService, BackgroundUpdateService} from './../api/BackgroundUpdateService';
import {IPilotsService, PilotsService} from './../api/PilotsService';

export class KernelCreator {
    public static create(): TypeIoc.IContainer {
        var kernel = TypeIoc.createBuilder();
        kernel.register<IAuthService>("IAuthService").as((c) => {
            var api = c.resolve<IApiCaller>("IApiCaller");
            return new AuthService(api);
        });

        kernel.register<IKeyInfoService>("IKeyInfoService").as((c) => {
            var api = c.resolve<IApiCaller>("IApiCaller");
            return new KeyInfoService(api);
        });
        kernel.register<IBackgroundUpdateService>("IBackgroundUpdateService").as((c) => {
            var api = c.resolve<IApiCaller>("IApiCaller");
            return new BackgroundUpdateService(api);
        });
        kernel.register<IPilotsService>("IPilotsService").as((c) => {
            var api = c.resolve<IApiCaller>("IApiCaller");
            return new PilotsService(api);
        });


        kernel.register<IApiCaller>("IApiCaller").as(() => new ApiCaller()).within(TypeIoc.Types.Scope.Container);
        kernel.register<IDeferredActionExecutor>("IDeferredActionExecutor").as(() => new DeferredActionExecutor()).within(TypeIoc.Types.Scope.Container);

        console.log('kernel created');
        return kernel.build();
    }
}