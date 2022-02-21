import {AuthGuard, } from "@nestjs/passport";

/**
 * Class JwtAuthGuard
 */
export class JwtAuthGuard extends AuthGuard( 'jwt' ){
}
