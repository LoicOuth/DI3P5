import jwtDecode from "jwt-decode"
import Token from "~~/core/Interfaces/Token.interface"
import User from "~~/core/Models/User.model"

export default function () {
    const config = useRuntimeConfig()
    const user = useAuthUser()
    const apiFetch = useApiFetch()
    const localePath = useLocalePath()
    const accessToken = useCookie("accessToken")
    const refreshToken = useCookie("refreshToken")

    const getCodeChallenge = async (verifier: string) => {
        const encoder = new TextEncoder()
        const data = encoder.encode(verifier)
        const a = await window.crypto.subtle.digest("SHA-256", data)
        return btoa(String.fromCharCode.apply(null, new Uint8Array(a))).replace(/\+/g, "-").replace(/\//g, "_").replace(/=+$/, "")
    }

    const redirectLogin = async (site?: string): Promise<void> => {
        const verifier = crypto.getRandomValues(new Uint32Array(10)).join("")
        if (!site) { window.localStorage.setItem("verifier", verifier) }

        const callbackUrl = encodeURIComponent(site ? `${config.CMS_CALLBACK_URL}?verifier=${verifier}&site=${site}` : `${config.BASE_URL}${localePath("/callback")}`)

        navigateTo(
            `${config.API_BASE_URL}/connect/authorize?scope=${config.AUTH_SCOPE}&response_type=code&client_id=${config.AUTH_CLIENT_ID}&redirect_uri=${callbackUrl}&code_challenge=${await getCodeChallenge(verifier)}&code_challenge_method=S256`,
            { external: true }
        )
    }

    const redirectManage = async () => {
        navigateTo(localePath("/account/dashboard"))
    }

    const login = async (code: string): Promise<void> => {
        const response = await apiFetch<User>("/connect/token", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: new URLSearchParams({
                grant_type: "authorization_code",
                code,
                client_id: config.AUTH_CLIENT_ID,
                client_secret: config.AUTH_CLIENT_SECRET,
                redirect_uri: `${config.BASE_URL}/callback`,
                code_verifier: window.localStorage.getItem("verifier")
            })
        })

        window.localStorage.removeItem("verifier")

        user.value = response
        accessToken.value = user.value.access_token
        refreshToken.value = user.value.refresh_token

        const parsedToken = parseJwt(accessToken.value)
        if (parsedToken.amr[0] === "pwd") {
            user.value.isExternal = false
        } else {
            user.value.isExternal = true
        }

        navigateTo(localePath("/account/dashboard"))
    }

    const logout = () => {
        user.value = accessToken.value = refreshToken.value = undefined

        navigateTo(localePath("/"))
    }

    const isConnected = () => {
        return accessToken.value !== undefined && accessToken.value
    }

    const getIsExteralUser = () => {
        return user.value.isExternal
    }

    const parseJwt = (token: string): Token => {
        return jwtDecode(token)
    }

    return {
        redirectLogin,
        redirectManage,
        login,
        logout,
        isConnected,
        getIsExteralUser
    }
}
