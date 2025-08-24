"use client"

import type React from "react"
import { useEffect, useState } from "react"
import { useParams, useNavigate } from "react-router-dom"
import type { Event } from "../types/Event"
import EventForm from "../components/EventForm"
import { ArrowLeft, Calendar, Clock, Edit3, Loader2 } from "../components/icons"
import { Button } from "../components/ui/button"
import { Badge } from "../components/ui/badge"
import { EventStatus } from "../types/Event"
import { getEventById, deleteEvent } from "../services/eventService"

const EventDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const [event, setEvent] = useState<Event | null>(null)
  const [isEditing, setIsEditing] = useState(false)
  const [loading, setLoading] = useState(true)

  const fetchEvent = async () => {
    if (!id) return
    try {
      setLoading(true)
      const data = await getEventById(id)
      setEvent(data)
    } catch (err) {
      console.error(err)
      alert("Error loading event")
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchEvent()
  }, [id])

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case "active":
        return "bg-emerald-100 text-emerald-700 border-emerald-200"
      case "upcoming":
        return "bg-amber-100 text-amber-700 border-amber-200"
      case "completed":
        return "bg-gray-100 text-gray-700 border-gray-200"
      case "cancelled":
        return "bg-red-100 text-red-700 border-red-200"
      default:
        return "bg-blue-100 text-blue-700 border-blue-200"
    }
  }

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("en-US", {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    })
  }

  const formatTime = (dateString: string) => {
    return new Date(dateString).toLocaleTimeString("en-US", {
      hour: "numeric",
      minute: "2-digit",
      hour12: true,
    })
  }

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-amber-50 via-white to-pink-50">
        <div className="text-center">
          <Loader2 className="w-12 h-12 animate-spin text-amber-500 mx-auto mb-4" />
          <p className="text-gray-600 text-lg">Loading event details...</p>
        </div>
      </div>
    )
  }

  if (!event) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-amber-50 via-white to-pink-50">
        <div className="text-center">
          <h2 className="text-2xl font-bold text-gray-900 mb-4">Event Not Found</h2>
          <p className="text-gray-600 mb-6">The event you're looking for doesn't exist.</p>
          <Button onClick={() => navigate("/")}>
            <ArrowLeft className="w-4 h-4 mr-2" />
            Back to Events
          </Button>
        </div>
      </div>
    )
  }

  if (isEditing) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-amber-50 via-white to-pink-50 py-8">
        <div className="max-w-4xl mx-auto px-6">
          <div className="mb-6">
            <Button variant="ghost" onClick={() => setIsEditing(false)} className="mb-4">
              <ArrowLeft className="w-4 h-4 mr-2" />
              Back to Details
            </Button>
          </div>
          <EventForm
            event={event}
            onSuccess={() => {
              setIsEditing(false)
              fetchEvent()
            }}
          />
        </div>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-amber-50 via-white to-pink-50">
      {/* Header */}
      <div className="bg-gradient-to-r from-amber-500 to-pink-500 text-white py-8">
        <div className="max-w-4xl mx-auto px-6">
          <Button variant="ghost" onClick={() => navigate("/")} className="mb-6 text-white hover:bg-white/20">
            <ArrowLeft className="w-4 h-4 mr-2" />
            Back to Events
          </Button>

          <div className="flex items-start justify-between">
            <div className="flex-1">
             <Badge className={`${getStatusColor(event.status as unknown as string)} mb-4`}>
  {EventStatus[event.status]}
</Badge>


              <h1 className="text-4xl font-bold mb-4">{event.title}</h1>
              <p className="text-xl text-white/90 leading-relaxed">{event.description}</p>
            </div>

            <Button onClick={() => setIsEditing(true)} variant="secondary" className="ml-6">
              <Edit3 className="w-4 h-4 mr-2" />
              Edit Event
            </Button>

<Button
      onClick={async () => {
        const confirmDelete = window.confirm("Are you sure you want to delete this event?");
        if (!confirmDelete) return;
        try {
          await deleteEvent(event.id as string);
          alert("Event deleted successfully");
          navigate("/");
        } catch (err) {
          console.error(err);
          alert("Failed to delete event");
        }
      }}
      variant="destructive"
    >
      Delete Event
    </Button>


          </div>
        </div>
      </div>

      {/* Event Details */}
      <div className="max-w-4xl mx-auto px-6 py-12">
        <div className="space-y-8">
          <div className="glass-card rounded-2xl p-8">
            <h2 className="text-2xl font-bold gradient-text mb-6">Event Details</h2>

            <div className="space-y-6">
              <div className="flex items-center">
                <Calendar className="w-6 h-6 text-amber-500 mr-4" />
                <div>
                  <div className="font-semibold text-gray-900">{formatDate(event.date)}</div>
                  <div className="text-gray-600">{formatTime(event.date)}</div>
                </div>
              </div>

              <div className="flex items-center">
                <Clock className="w-6 h-6 text-emerald-500 mr-4" />
                <div>
                  <div className="font-semibold text-gray-900">Duration</div>
                  <div className="text-gray-600">3 hours (estimated)</div>
                </div>
              </div>
            </div>
          </div>

          {/* About & Highlights */}
          <div className="glass-card rounded-2xl p-8">
            <h3 className="text-xl font-bold gradient-text mb-6">About This Event</h3>
            <div className="prose prose-gray max-w-none">
              <p className="text-gray-600 leading-relaxed mb-4">{event.description}</p>
              
            </div>
          </div>

          <div className="glass-card rounded-2xl p-8">
            <h3 className="text-xl font-bold gradient-text mb-6">Event Highlights</h3>
            <ul className="space-y-3">
              <li className="flex items-center text-gray-600">
                <div className="w-2 h-2 bg-amber-500 rounded-full mr-3"></div>
                Expert speakers and industry leaders
              </li>
              <li className="flex items-center text-gray-600">
                <div className="w-2 h-2 bg-pink-500 rounded-full mr-3"></div>
                Networking opportunities
              </li>
              <li className="flex items-center text-gray-600">
                <div className="w-2 h-2 bg-blue-500 rounded-full mr-3"></div>
                Hands-on workshops and sessions
              </li>
              <li className="flex items-center text-gray-600">
                <div className="w-2 h-2 bg-emerald-500 rounded-full mr-3"></div>
                Complimentary refreshments
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  )
}

export default EventDetails
