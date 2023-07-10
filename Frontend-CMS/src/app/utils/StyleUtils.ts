export const getColorFromClass = (colorClass: string): string => {
   let regex = /#([a-fA-F0-9]{3,6})/
   return colorClass.match(regex)![0]
}

export const getValueInString = (string: string): string => {
   return string.replace(/\D/g, "")
}