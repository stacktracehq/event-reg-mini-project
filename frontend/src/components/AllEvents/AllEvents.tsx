import React from "react";
import { EventDTO } from "../../models/models";
import axios from "axios";

export class AllEvents extends React.Component<{}, EventDTO[]> {
    constructor(props: any) {
        super(props);
        this.state = []
    }

     componentDidMount() {
         console.log("mounted");
        axios.request<EventDTO[]>({
            url: 'https://localhost:5001/v1/events',

        })
            .then(response => {
                this.setState(response.data);
            })
            .catch(error => {
                console.log(error);
            })
    }

    mapEvents = (): JSX.Element[] => {
        return this.state.map((anEvent) => {
            return (
                <ul key={anEvent.id}>
                    <li>{anEvent.id}</li>
                    <li>{anEvent.title}</li>
                </ul>
            )
        });

    }

    renderEvents() {
        if (this.state.length === 0) {
            return (
                <p>
                    There are no events!
                </p>
            );
        } else {
            return(
                <p>
                    There are {this.state.length} events!
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