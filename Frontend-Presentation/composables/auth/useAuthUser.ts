import User from "~~/core/Models/User.model"

export default function () {
    return useState<User | null>("users", () => null)
}
