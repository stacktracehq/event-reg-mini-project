import React from "react";
import { EventDTO } from "../../models/models";
import styles from './EventList.module.css'
import axios from "axios";
import {  Link, RouteComponentProps } from "react-router-dom"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
    faTrashAlt,
    faEdit
 } from "@fortawesome/free-regular-svg-icons";
 import { faSpinner } from "@fortawesome/free-solid-svg-icons"
import { faPlusSquare } from "@fortawesome/free-regular-svg-icons";

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
                <tr key={anEvent.id}>
                    <td><Link to={`/event/${anEvent.id}`}>{anEvent.title}</Link></td>
                    <td>
                        <Link to={`/event/${anEvent.id}/edit`} className={styles.button}>
                            <FontAwesomeIcon icon={faEdit} fixedWidth size="sm" />{" "}
                            Edit
                        </Link>
                        <button
                            type="button"
                            className={[ styles.button, styles.delete ].join(' ')}
                            onClick={() =>
                                this.handleDelete(anEvent.id)
                            }
                        >
                            <FontAwesomeIcon icon={faTrashAlt} fixedWidth size="sm" />{" "}
                            Delete
                        </button>
                    </td>
                </tr>
            )
        });
    }

    renderEvents() {
        if (this.state.items.length === 0) {
            return (
                <div className={styles.loadingContainer}>
                <p className={styles.loading}>
                    <FontAwesomeIcon icon={faSpinner} spin fixedWidth />
                    Loading...
                </p>
                </div>
            );
        } else {
            return(
                <div>
                    {/* <p>There are {this.state.items.length} events!</p> */}
                    <p className={styles.newEventP}>
                        <Link to="/new" className={styles.newEvent}>
                            <FontAwesomeIcon icon={faPlusSquare} fixedWidth size="sm" />{" "}
                                Create New Event
                        </Link>
                    </p>

                    <table>
                        <tbody>
                            {this.mapEvents()}
                        </tbody>
                    </table>

                </div>
            );
        }
    }

    public render() {
        return (
            <div className={styles.main}>
                <h1>Carlie's Stupendous and Amazing Event Registration Project</h1>

                {this.renderEvents()}
            </div>
        )
    }
}

export default EventList;