# Event Registration Mini Project

An internal project where Carlie can learn the key technologies and approaches
used on the Blackbaud project. The goal is to build a roughly similar, but vastly _simpler_,
version of the Online Registrations product we are building for Blackbaud.

The strategies for how we plan to run this mini-project are described in [THE_PLAN.md](THE_PLAN.md),
so give that a read. Obviously nothing there is set in stone.

This is currently a _private_ repository since we may steal some of the Blackbaud
project code. Once we get to the end of the project we can make it a public
repository if that seems appropriate.

## Useful Links

* [Project Board](https://github.com/stacktracehq/event-reg-mini-project/projects/2)
* [Milestones](https://github.com/stacktracehq/event-reg-mini-project/milestones?direction=asc&sort=due_date&state=open)

---

## How To Work With This Thing

To run this, you will need:

* [C# installed](https://dotnet.microsoft.com/download)
* PostgreSQL installed:

```
$ sudo apt-get update
$ sudo apt-get install postgresql postgresql-contrib
```

### Set Up PosgreSQL

Run posgreSQL with the command: `psql`.

**In Postgres** you will need to:

* Create a database called `stacktrace`:

```
CREATE DATABASE stacktrace;
```

* Create a table called `event_management`, with the following structure:

```
    CREATE TABLE event_management (
        id uuid PRIMARY KEY,
        title text,
        description text,
        event_location text,
        event_start_date timestamp,
        event_end_date timestamp,
        registration_open_date timestamp,
        registration_close_date timestamp
    );
```

* Change you postgres id and username in `backend/src/Whiteboard.Registration.Web/appsettings.json` (line 10).

### Using the API

To run the server, navigate to `backend/src/Whiteboard.Registration.Web` and type in the command:
```dotnet run```

The default setting will have the server running on `https://localhost:5001`

This will run the EventMangementController located at `backend/src/Whiteboard.Registration.Web/Controllers/EventManagementController.cs`

It has the following actions:

* GET (route: /v1/events)
    * Gets all events, displays the ID and title.
    * You are also able to filter events by title, for instance: `https://localhost:5001/v1/events?title=carlie` - this will display all events with "carlie" in the title.
* GET by id (route: /v1/events/{id})
    * Gets individual event
* POST (route: /v1/events)
    * Adds new event to the repository
* PUT (route: /v1/events/{id})
    * Updates individual event
* DELETE (route: /v1/events/{id})
    * Deletes individual event