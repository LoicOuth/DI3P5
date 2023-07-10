export default interface IAuth {
   access_token: string
   access_expiration: Date
   refresh_token: string
   scope: string[]
}
