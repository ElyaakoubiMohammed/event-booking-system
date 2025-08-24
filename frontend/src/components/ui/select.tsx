import * as React from "react"
import { cn } from "../../lib/utils"

const Select = React.forwardRef<HTMLSelectElement, React.SelectHTMLAttributes<HTMLSelectElement>>(
  ({ className, children, ...props }, ref) => {
    return (
      <select
        className={cn(
          "flex h-11 w-full rounded-lg border-2 border-gray-200 bg-white px-4 py-2 text-sm transition-all focus:border-amber-400 focus:outline-none focus:ring-2 focus:ring-amber-100 disabled:cursor-not-allowed disabled:opacity-50",
          className,
        )}
        ref={ref}
        {...props}
      >
        {children}
      </select>
    )
  },
)
Select.displayName = "Select"

const SelectContent = ({ children }: { children: React.ReactNode }) => children
const SelectItem = React.forwardRef<HTMLOptionElement, React.OptionHTMLAttributes<HTMLOptionElement>>(
  ({ className, children, ...props }, ref) => {
    return (
      <option className={cn("py-2 px-4", className)} ref={ref} {...props}>
        {children}
      </option>
    )
  },
)
SelectItem.displayName = "SelectItem"

const SelectTrigger = Select
const SelectValue = ({ placeholder }: { placeholder?: string }) => (
  <option value="" disabled hidden>
    {placeholder}
  </option>
)

export { Select, SelectContent, SelectItem, SelectTrigger, SelectValue }
