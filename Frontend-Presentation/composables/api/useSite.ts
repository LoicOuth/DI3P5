import Deployment from "~~/core/Interfaces/Deployment.interface"
import Site from "~~/core/Interfaces/Site.interface"

export default function () {
    const apiFetch = useApiFetch()
    const URL = "/api/site"

    const create = async (name: string, description: string) => {
        await apiFetch(URL, {
            method: "POST",
            body: {
                name,
                description
            }
        })
    }

    const remove = async (id: string) => {
        await apiFetch(`${URL}/${id}`, {
            method: "DELETE"
        })
    }

    const update = async (id: string, name: string, description: string) => {
        await apiFetch(`${URL}/${id}`, {
            method: "PUT",
            body: {
                id,
                name,
                description
            }
        })
    }

    const getAllSites = async () => {
        return apiFetch<Site[]>(URL, {
            method: "GET"
        })
    }

    const getLastDeployments = async () => {
        return await apiFetch<Deployment[]>(`${URL}/deploy`, {
            method: "GET"
        })
    }

    return {
        create,
        remove,
        update,
        getAllSites,
        getLastDeployments
    }
}
