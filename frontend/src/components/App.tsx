
import React from 'react';
import styles from "./App.module.css";
import {
    Route,
    Link,
    BrowserRouter as Router,
    Switch
} from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlusSquare } from "@fortawesome/free-regular-svg-icons";
import NewEvent from "./NewEvent/NewEvent";
import EventList from "./EventList/EventList";
import ViewEvent from "./ViewEvent/ViewEvent";
import EditEvent from './EditEvent/EditEvent';


const App: React.FC = () => {
  return (
    <Router>
        <nav>
            <ul>
                <li>
                    <Link to="/" className={[ styles.menuItem, styles.brand ].join(" ")}>
                        Carlie's Events
                    </Link>
                </li>
                <li>
                    <Link to="/" className={styles.menuItem}>All Events</Link>
                </li>
                <li>
                    <Link to="/new" className={styles.newEvent}>
                        <FontAwesomeIcon icon={faPlusSquare} fixedWidth size="sm" />{" "}
                        Create New Event
                    </Link>
                </li>
            </ul>
        </nav>
            <Switch>
                <Route exact path="/event/:id/edit" component={EditEvent} />
                <Route path="/event/:id" component={ViewEvent} />
                <Route path="/new" component={NewEvent} />
                <Route exact path="/" component={EventList} />
            </Switch>
    </Router>
)
}

export default App;