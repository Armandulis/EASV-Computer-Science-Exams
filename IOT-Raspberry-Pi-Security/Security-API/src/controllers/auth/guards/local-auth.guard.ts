import {AuthGuard, } from "@nestjs/passport";

/**
 * Class LocalAuthGuard
 */
export class LocalAuthGuard extends AuthGuard( 'local' ){
}
