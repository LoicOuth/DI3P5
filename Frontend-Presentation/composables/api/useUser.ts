import UserInfo from "~~/core/Models/UserInfo.model"

export default function () {
    const apiFetch = useApiFetch()
    const URL = "/api/user"

    const downloadPersonalInfo = async () => {
        await apiFetch(URL + "/download", {
            method: "GET"
        })
    }

    const sendEmail = async (to: string, subject: string, message: string) => {
        await apiFetch(URL + "/email", {
            method: "POST",
            body: {
                to,
                subject,
                message
            }
        })
    }

    const updatePersonalInfo = async (newUsername: string, newEmail: string) => {
        await apiFetch(URL, {
            method: "PUT",
            body: {
                newUsername,
                newEmail
            }
        })
    }

    const updatePassword = async (oldPassword: string, newPassword: string) => {
        await apiFetch(URL + "/password", {
            method: "PUT",
            body: {
                oldPassword,
                newPassword
            }
        })
    }

    const getUserInfo = async () => {
        return await apiFetch<UserInfo>("/api/user/")
    }

    return {
        downloadPersonalInfo,
        sendEmail,
        updatePersonalInfo,
        updatePassword,
        getUserInfo
    }
}
