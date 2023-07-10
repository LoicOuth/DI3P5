import jwtDecode from "jwt-decode"
import Token from "~~/core/Interfaces/Token.interface"
import User from "~~/core/Models/User.model"

export default defineNuxtRouteMiddleware(() => {
    const currentUser = useAuthUser()
    if (currentUser.value === null) {
        const user = checkCookie()
        if (user !== null) {
            currentUser.value = user
        } else {
            return navigateTo("/")
        }
    }
})

const checkCookie = (): User => {
    const accessToken = useCookie("accessToken")
    const refreshToken = useCookie("refreshToken")
    if (accessToken.value && refreshToken.value) {
        const decodeAccess = parseJwt(accessToken.value)
        const isExternal = decodeAccess.amr[0] !== "pwd"
        if (new Date(decodeAccess.exp * 1000).getTime() < new Date().getTime()) {
            accessToken.value = refreshToken.value = undefined
            return null
        }
        return new User(accessToken.value, refreshToken.value, new Date(decodeAccess.exp * 1000), decodeAccess.scope, isExternal)
    }
    return null
}

const parseJwt = (token: string): Token => {
    return jwtDecode(token)
}
