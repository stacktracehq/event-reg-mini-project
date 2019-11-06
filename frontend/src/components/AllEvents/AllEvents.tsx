import React from "react";
import { EventDTOs, EventDTO } from "../../models/index";
import axios from "axios";

export class AllEvents extends React.Component<{}, EventDTOs> {
    constructor(props: any) {
        super(props);
        this.state = {
            events: []
        }
    }

     componentDidMount() {
         console.log("mounted");
        axios.request<EventDTO[]>({
            url: 'https://localhost:5001/v1/events',

        })
            .then(response => {
                const { data } = response;
                this.setState({ events: data });
            })
            .catch(error => {
                console.log(error);
            })
    }

    mapEvents = (): JSX.Element[] => {
        return this.state.events.map((anEvent) => {
            return (
                <ul key={anEvent.id}>
                    <li>{anEvent.id}</li>
                    <li>{anEvent.title}</li>
                </ul>
            )
        });

    }

    renderEvents() {
        if (this.state.events.length === 0) {
            return (
                <p>
                    There are no events!
                </p>
            );
        } else {
            return(
                <p>
                    There are {this.state.events.length} events!
                    {this.mapEvents()}
                </p>
            );
        }
    }

    public render() {
        console.log("rendering");
        return (
            <div>
                <h1>Carlie's Stupendous and Amazing Event Registration Project</h1>

                    {this.renderEvents()}

            </div>
        )
    }
}

export default AllEvents;