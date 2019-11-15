import React from "react";
import { EventDTO } from "../../models/models";
import axios from "axios";
import {  Link, RouteComponentProps } from "react-router-dom"

export class EventList extends React.Component<{} & RouteComponentProps,{ items: EventDTO[] }> {
    private mounted: boolean;

    constructor(props: RouteComponentProps) {
        super(props);
        this.mounted = false;
        this.state = { items: [] }
    }

     componentDidMount() {
        this.mounted = true;
        axios.request<EventDTO[]>({
            url: 'https://localhost:5001/v1/events',

        })
            .then(response => {
                if (this.mounted) {
                    this.setState({ items: response.data });
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    componentWillUnmount() {
        this.mounted = false;
    }

    handleDelete(eventId: string) {
        axios.delete(
            `https://localhost:5001/v1/events/${eventId}`
        )
        .then(() => {
            this.removeEvent(eventId);
        })
        .catch(error => {
            console.log(error);
        })
    }

    removeEvent(eventId: string) {
        let eventsCopy = [...this.state.items]
        let index = eventsCopy.findIndex(e => e.id === eventId);
        if (index !== -1) {
            eventsCopy.splice(index, 1);
            this.setState({ items: eventsCopy });
        }
    }

    mapEvents = (): JSX.Element[] => {
        return this.state.items.map((anEvent) => {
            return (
                <tbody key={anEvent.id}>
                    <tr>
                        <td>{anEvent.id}</td>
                        <td><Link to={`/event/${anEvent.id}`}>{anEvent.title}</Link></td>
                        <td>
                            <Link to={`/event/${anEvent.id}`}>View</Link>
                            <Link to={`/event/${anEvent.id}/edit`}>Edit</Link>
                            <button
                                type="button"
                                className="delete"
                                onClick={() =>
                                    this.handleDelete(anEvent.id)
                                }
                            >
                                Delete
                            </button>
                        </td>
                    </tr>
                </tbody>
            )
        });
    }

    renderEvents() {
        if (this.state.items.length === 0) {
            return (
                <p>
                    There are no events!
                </p>
            );
        } else {
            return(
                <div>
                    <p>There are {this.state.items.length} events!</p>
                    <table>
                        <thead>
                            <tr>
                                <th>Event Id</th>
                                <th>Event Title</th>
                                <th></th>
                            </tr>
                        </thead>
                            {this.mapEvents()}
                    </table>
                </div>
            );
        }
    }

    public render() {
        return (
            <div>
                <h1>Carlie's Stupendous and Amazing Event Registration Project</h1>

                {this.renderEvents()}
            </div>
        )
    }
}

export default EventList;