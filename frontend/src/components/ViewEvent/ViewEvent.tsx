import React from "react";
import axios from "axios";
import { Event, EventId } from "../../models/models";
import { RouteComponentProps, Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt, faEdit } from "@fortawesome/free-regular-svg-icons";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";
import styles from "./ViewEvent.module.css";
import { formatDateTimeForPrettyPrint } from "../../utils/dateTimeHelpers"

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
                        {formatDateTimeForPrettyPrint(this.state.eventStartDate.value)}
                    </p>
                    <p className={styles.spacing}>
                        <span className={styles.fieldName}>Event End Date: </span>
                        {formatDateTimeForPrettyPrint(this.state.eventEndDate.value)}
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