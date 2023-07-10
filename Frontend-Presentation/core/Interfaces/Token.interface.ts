export default interface Token {
    exp: number, // expiration
    scope: string[], // scope
    aud: string, // Audience
    client_id: string // client
    amr: string[] // isExternal
}
