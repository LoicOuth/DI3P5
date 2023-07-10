export default function () {
    const { API_BASE_URL } = useRuntimeConfig()
    const user = useAuthUser()

    const apiFetch = $fetch.create({
        baseURL: API_BASE_URL,
        headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            Authorization: "Bearer " + user.value?.access_token
        },
        async onRequestError (error) {
            console.log("Request error", error)
        }
    })

    return apiFetch
}
