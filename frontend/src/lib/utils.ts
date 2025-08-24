export type ClassValue = string | number | null | undefined | Record<string, any> | ClassValue[]

function clsx(...inputs: ClassValue[]): string {
  return inputs
    .filter(Boolean)
    .map((input) => {
      if (typeof input === "string" || typeof input === "number") {
        return input.toString()
      }
      if (typeof input === "object" && input !== null) {
        return Object.entries(input)
          .filter(([, value]) => Boolean(value))
          .map(([key]) => key)
          .join(" ")
      }
      return ""
    })
    .join(" ")
    .trim()
}

function twMerge(...classLists: string[]): string {
  // Simple implementation - in a real app you'd use the actual tailwind-merge
  return classLists.filter(Boolean).join(" ")
}

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(...inputs))
}
