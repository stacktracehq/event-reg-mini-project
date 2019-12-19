import React from "react";
import axios from "axios";
import { Event, EventId } from "../../models/models";
import { RouteComponentProps, Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-regular-svg-icons";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";
import styles from "./ViewEvent.module.css";

export class ViewEvent extends React.Component<RouteComponentProps<EventId>, Event> {
    constructor(props: RouteComponentProps<EventId>) {
        super(props);
        this.state = {
            id: "",
            title: {value: ""},
            description: {value: ""},
            eventLocation: {value: ""},
            eventStartDate: {value: ""},
            eventEndDate: {value: ""},
            registrationOpenDate: {value: ""},
            registrationCloseDate: {value: ""},
        }
    }

    componentDidMount() {
        axios.request<Event>({
            url: `https://localhost:5001/v1/events/${this.props.match.params.id}`
        })
            .then(response => {
                const { data } = response;
                this.setState({
                    id: data.id,
                    title: data.title,
                    description: data.description,
                    eventLocation: data.eventLocation,
                    eventStartDate:data.eventStartDate,
                    eventEndDate: data.eventEndDate,
                    registrationOpenDate: data.registrationOpenDate,
                    registrationCloseDate: data.registrationCloseDate
                 });
            })
            .catch(error => {
                console.log(error)
            })

    }

    handleDelete() {
        axios.delete(
            `https://localhost:5001/v1/events/${this.props.match.params.id}`
        )
        .then(() => {
            const { history } = this.props;
            history.push("/");
        })
        .catch(error => {
            console.log(error);
        })
    }

    formatDateTime(dateTimeStr: Date | string) {
        if (typeof dateTimeStr === 'string') {
            dateTimeStr = new Date(dateTimeStr);
        }
        let year = dateTimeStr.getFullYear();
        let month = dateTimeStr.getMonth();
        let day = dateTimeStr.getDay();
        let hour = dateTimeStr.getHours();
        let minutes = dateTimeStr.getMinutes();
        let ampm = "am";

        // monthNames to get the name of the month for output
        const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

        // converts time to either am or pm
        if (hour > 12) {
            hour = hour - 12;
            ampm = "pm";
        }

        let time = `${hour}:${minutes}${ampm}`;

        // add leading 0 if minutes less than 10
        if (minutes < 10) {
            time = `${hour}:0${minutes}${ampm}`
        }

        return `${day} ${monthNames[month]} ${year}, ${time}`;
    }

    renderEvent() {
        if (this.state.id === "") {
            return (
                <div className={[ styles.main, styles.loadingContainer ].join(" ")}>
                    <p className={styles.loading}>
                        <FontAwesomeIcon icon={faSpinner} spin fixedWidth />
                        Loading...
                    </p>
                </div>
             )
        } else {
            return (

                <div className={styles.main}>
                    <h1>
                        {this.state.title.value}
                    </h1>
                    <p>
                        {this.state.description.value}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Event Location: </span>
                        {this.state.eventLocation.value}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Event Start Date: </span>
                        {this.formatDateTime(this.state.eventStartDate.value)}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Event End Date: </span>
                        {this.state.eventEndDate.value.toString().substr(0,10)}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Registration Open Date: </span>
                        {this.state.registrationOpenDate.value.toString().substr(0,10)}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Registration Close Date: </span>
                        {this.state.registrationCloseDate.value.toString().substr(0,10)}
                    </p>
                    <div className={styles.spacing}>
                        <Link to={`/event/${this.state.id}/edit`} className={styles.button}>
                            <FontAwesomeIcon icon={faEdit} fixedWidth size="sm" />{" "}
                            Edit
                        </Link>
                        <button
                                type="button"
                                className={[styles.button, styles.delete].join(" ")}
                                onClick={() =>
                                    this.handleDelete()
                                }
                            >
                                <FontAwesomeIcon icon={faTrashAlt} fixedWidth size="sm" />{" "}
                                Delete
                            </button>
                    </div>
                </div>
            )
        }
    }

    public render() {
        return (
            <div>
                {this.renderEvent()}
            </div>
        )
  }
}

export default ViewEvent;