export default function () {
    const stringToLocaleDate = (stringDate: string): string => {
        const date = new Date(stringDate)
        return date.toLocaleString().slice(0, -3)
    }

    const elapsedTimeString = (start, end) => {
        const startDate = new Date(start)
        const endDate = new Date(end)

        let diffInSeconds = Math.abs(endDate.getTime() - startDate.getTime()) / 1000

        const hours = Math.floor(diffInSeconds / 3600) % 24
        diffInSeconds -= hours * 3600

        const minutes = Math.floor(diffInSeconds / 60) % 60
        diffInSeconds -= minutes * 60

        const seconds = Math.floor(diffInSeconds % 60)

        let result = ""

        if (hours > 0) {
            result += hours + "h"
        }

        if (minutes > 0) {
            result += minutes + "m"
        }

        if (seconds > 0) {
            result += seconds + "s"
        }

        return result
    }

    return {
        stringToLocaleDate,
        elapsedTimeString
    }
}
