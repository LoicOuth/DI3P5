export default class User {
    access_token: string
    access_expiration: Date
    refresh_token: string
    scope: string[]
    isExternal: boolean

    constructor (access?: string, refresh?: string, accessExpiration ?: Date, scope?: string[], isExternal?: boolean) {
        this.access_token = access
        this.access_expiration = accessExpiration
        this.scope = scope
        this.refresh_token = refresh
        this.isExternal = isExternal
    }

    toJSON () {
        return { ...this }
    }
}
