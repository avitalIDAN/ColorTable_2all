@Code
    ViewData("Title") = "אודות"
End Code

<main aria-labelledby="title">
    <h2 id="title">@ViewData("Title").</h2>
    <h3>@ViewData("Message")</h3>

    <p>
        ניהול טבלת נתונים פשוטה, 
        <br />
        השדות בטבלת הצבעים : שם הצבע, מחיר, סדר הצגה, האם הצבע במלאי.
        <br />
        אפשריות לניהל נתונים בטבלה הצבעים :
        <br />
        להכניס רשומות חדשות - insert
        <br />
        לעדכן אותן - update
        <br />
        ולמחוק – delete
    </p>
</main>
