"use client"

import type React from "react"
import { useNavigate } from "react-router-dom"
import type { Event } from "../types/Event"
import { Calendar, Users, MapPin, Clock, TrendingUp, Star } from "./icons"
import { Badge } from "./ui/badge"
import { EventStatus } from "../types/Event"

interface EventCardProps {
  event: Event
}

const EventCard: React.FC<EventCardProps> = ({ event }) => {
  const navigate = useNavigate()

  const getStatusColor = (status: string) => {
    switch (status) {
      case "Scheduled":
        return "bg-emerald-100 text-emerald-700 border-emerald-200"
      case "Cancelled":
        return "bg-red-100 text-red-700 border-red-200"
      case "Completed":
        return "bg-gray-100 text-gray-700 border-gray-200"
      default:
        return "bg-blue-100 text-blue-700 border-blue-200"
    }
  }

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("en-US", {
      month: "short",
      day: "numeric",
      year: "numeric",
    })
  }

  const formatTime = (dateString: string) => {
    return new Date(dateString).toLocaleTimeString("en-US", {
      hour: "numeric",
      minute: "2-digit",
      hour12: true,
    })
  }

  return (
    <div
      onClick={() => navigate(`/events/${event.id}`)}
      className="group glass-card rounded-2xl p-6 cursor-pointer transition-all duration-300 hover:shadow-2xl hover:scale-105 floating-animation relative overflow-hidden"
    >
      {/* Background gradient overlay */}
      <div className="absolute inset-0 bg-gradient-to-br from-amber-500/5 to-pink-500/5 opacity-0 group-hover:opacity-100 transition-opacity duration-300" />

     {/* Status badge */}
<div className="flex justify-between items-start mb-4">
  <Badge className={`${getStatusColor(event.status as unknown as string)} font-medium px-3 py-1`}>
    {EventStatus[event.status]}
  </Badge>
  <div className="flex items-center space-x-1 text-amber-500">
    <Star className="w-4 h-4 fill-current" />
    <span className="text-sm font-medium">4.8</span>
  </div>
</div>


      {/* Event title */}
      <h3 className="text-xl font-bold text-gray-900 mb-3 group-hover:gradient-text transition-all duration-300">
        {event.title}
      </h3>

      {/* Event description */}
      <p className="text-gray-600 text-sm mb-4 line-clamp-2 leading-relaxed">{event.description}</p>

      {/* Event details grid */}
      <div className="space-y-3 mb-4">
        <div className="flex items-center text-gray-600 text-sm">
          <Calendar className="w-4 h-4 mr-3 text-amber-500" />
          <span className="font-medium">{formatDate(event.date)}</span>
          <Clock className="w-4 h-4 ml-4 mr-2 text-amber-500" />
          <span>{formatTime(event.date)}</span>
        </div>

        <div className="flex items-center text-gray-600 text-sm">
          <MapPin className="w-4 h-4 mr-3 text-pink-500" />
          <span className="truncate">Event Venue</span>
        </div>

        <div className="flex items-center justify-between">
          <div className="flex items-center text-gray-600 text-sm">
            <Users className="w-4 h-4 mr-3 text-blue-500" />
            <span>
              {event.bookedSeats}/{event.capacity} booked
            </span>
          </div>

          <div className="flex items-center text-emerald-600 text-sm font-semibold">
            <TrendingUp className="w-4 h-4 mr-1" />
            <span>{Math.round((event.bookedSeats / event.capacity) * 100)}% full</span>
          </div>
        </div>
      </div>

      <div className="flex items-center justify-between pt-4 border-t border-gray-100">
        <div className="text-lg font-bold gradient-text">{event.capacity - event.bookedSeats} seats left</div>
        <div className="text-sm text-gray-500">available</div>
      </div>

      {/* Hover shimmer effect */}
      <div className="absolute inset-0 shimmer opacity-0 group-hover:opacity-100 pointer-events-none" />
    </div>
  )
}

export default EventCard
