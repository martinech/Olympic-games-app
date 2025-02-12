# 🏅 Olympic Games App

## 📌 Overview
The **Olympic Games App** is a web app that helps different users manage the various aspects which need to be addressed when organizing sports competitions.
More specifically, this app let's the user register, login and depending on wether the user is admin or not different features will be available.

This application was developed as an assignment for **Programación 3** at **ORT University**.

## 🚀 Features
- **JWT Authorized Login** – Different responsibilities assigned based on user’s role (admin/scorer).
- **Athlete Discipline Assignment** – The scorer can assign a sport discipline to an athlete.
- **Discipline Creation** – Disciplines can be created while considering required validations.
- **Alphabetical Discipline Listing** – Disciplines are listed in ascending order alphabetically.
- **Event Creation** – Events can be created while considering required validations.
- **Score Assignment** – Scores can be assigned to athletes.
- **Athlete Event History** – Given an athlete’s ID, a list of all the events they participated in will be shown.
- **Discipline Event Listing** – Given a discipline’s ID, a list of all events in that discipline will be shown.
- **Events Between Dates** – Given two dates, a list of all the events within that date range will be shown.
- **Event Search by Name** – Given a name, a list of all events containing that name will be shown.
- **Score Range Filtering** – Given a score range, a list of all events where athletes had scores within that range will be shown.
- **Discipline Search by Name** – Given a name, a list of all disciplines containing that name will be shown.
- **Discipline Retrieval by ID** – Given a discipline’s ID, if it exists, it will be shown.
- **Athlete Discipline Listing** – Given a discipline’s ID, a list of all athletes competing in that discipline will be shown.


## 🛠️ Technologies Used
- **Frontend:** HTML, CSS, JavaScript
- **Backend:** C# ASP.NET
- **Database:** EntityFramework
- **Other Tools:** Git, Postman

## 📂 Project Structure
```
Olympic-games-app/
├── Obligatorio2/
|   ├──Controllers/
|   ├──Models/
|   ├──Properties/
|   ├──Views/
|   ├──wwwroot/
|   ├──Obligatorio2.csproj
|   ├──Program.cs
|   ├──appsettings.Development.json
|   ├──appsettings.json
├── .gitattributes
├── .gitignore
├── LICENSE
├── Obligatorio2.sln
└── README.md
```

## 🚀 Installation & Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/martinech/Olympic-games-app
   cd Olympic-games-app
   ```
2. Build DataBase:
   ```sh
   From VS Code:
      Run 'Add-Migration NewMigration' in Package Manager Console
      Then update-database
   ```
3. Check connection string:
   ```sh
   Make sure the connection string in appsettings.json points to the correct server
   ```
4. Run app:
   ```sh
   Debug and run
   ```

## 👨‍💻 Contributors
This project was developed in collaboration with **Pablo Rodriguez** as part of the **Programación 3** course at **ORT University**.

## 📄 License
This project is licensed under the [MIT License](LICENSE).

## 📬 Contact
For questions or feedback, feel free to reach out:
- **Email:** martin.echenique@hotmail.com
- **GitHub Issues:** Open an issue on the repository

---
*Happy Coding! 🚀*


