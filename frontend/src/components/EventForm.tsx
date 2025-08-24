"use client"

import type React from "react"
import { type ChangeEvent, useState, useEffect } from "react"
import { createEvent, updateEvent } from "../services/eventService"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "./ui/button"
import { Input } from "./ui/input"
import { Textarea } from "./ui/textarea"
import { Label } from "./ui/label"
import { Select } from "./ui/select"
// Use normal import, not 'import type'
import { type Event, EventStatus } from "../types/Event"

interface Props {
  onSuccess: () => void
  event?: Event
}

const EventForm: React.FC<Props> = ({ onSuccess, event }) => {
  const navigate = useNavigate()
  const { id } = useParams<{ id: string }>()
  const [formData, setFormData] = useState<Event>({
    id: "",
    title: "",
    description: "",
    date: "",
    capacity: 0,
    bookedSeats: 0,
    status: EventStatus.Scheduled,
  })

  useEffect(() => {
    if (event) {
      // Convert date to input datetime-local format
      const formattedDate = new Date(event.date).toISOString().slice(0, 16)
      setFormData({ ...event, date: formattedDate })
    }
  }, [event])

  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: name === "capacity" || name === "bookedSeats" ? Number(value) : value,
    }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    const statusMap: Record<string, EventStatus> = {
      Scheduled: EventStatus.Scheduled,
      Cancelled: EventStatus.Cancelled,
      Completed: EventStatus.Completed,
    }

    const payload: Event = {
      ...formData,
      date: new Date(formData.date).toISOString(),
      status: statusMap[formData.status], // numeric value matching backend
    }

    console.log("Sending update:", payload)

    try {
      if (id) await updateEvent({ ...payload, id })
      else await createEvent(payload)
      onSuccess()
      navigate("/")
    } catch (err: any) {
      console.error("Validation errors:", err.response?.data.errors)
    }
  }

  return (
    <div className="glass-card rounded-2xl p-8 max-w-2xl mx-auto">
      <h2 className="text-2xl font-bold gradient-text mb-6">{id ? "Update Event" : "Create New Event"}</h2>

      <form onSubmit={handleSubmit} className="space-y-6">
        <div className="space-y-2">
          <Label htmlFor="title">Event Title</Label>
          <Input
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            placeholder="Enter event title"
            required
          />
        </div>

        <div className="space-y-2">
          <Label htmlFor="description">Description</Label>
          <Textarea
            id="description"
            name="description"
            value={formData.description}
            onChange={handleChange}
            placeholder="Describe your event"
            required
          />
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <div className="space-y-2">
            <Label htmlFor="date">Date & Time</Label>
            <Input id="date" type="datetime-local" name="date" value={formData.date} onChange={handleChange} required />
          </div>

          <div className="space-y-2">
            <Label htmlFor="capacity">Event Capacity</Label>
            <Input
              id="capacity"
              name="capacity"
              type="number"
              value={formData.capacity}
              onChange={handleChange}
              placeholder="Maximum attendees"
              min="1"
              required
            />
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <div className="space-y-2">
            <Label htmlFor="bookedSeats">Booked Seats</Label>
            <Input
              id="bookedSeats"
              type="number"
              name="bookedSeats"
              value={formData.bookedSeats}
              onChange={handleChange}
              min="0"
              max={formData.capacity}
              required
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="status">Status</Label>
            <Select name="status" value={formData.status} onChange={handleChange}>
              <option value="Scheduled">Scheduled</option>
              <option value="Cancelled">Cancelled</option>
              <option value="Completed">Completed</option>
            </Select>
          </div>
        </div>

        <div className="flex gap-4 pt-4">
          <Button type="submit" className="flex-1">
            {id ? "Update Event" : "Create Event"}
          </Button>
          <Button type="button" variant="outline" onClick={() => navigate("/")} className="flex-1">
            Cancel
          </Button>
        </div>
      </form>
    </div>
  )
}

export default EventForm
