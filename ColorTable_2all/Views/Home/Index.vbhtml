@Code
    ViewData("Title") = "טבלת צבעים"
End Code

@ModelType List(Of Color)

<main>
    <section class="row" aria-labelledby="aspnetTitle">
        <h1 id="title">מערכת לניהול טבלת צבעים</h1>
        <p class="lead">
            ניהול טבלת נתונים פשוטה, השדות בטבלת הצבעים : שם הצבע, מחיר, סדר הצגה, האם הצבע במלאי.
        </p>
    </section>

    <div>
        <h2>טבלת הצבעים</h2>
        <table class="table">
            <thead>
                <tr>
                    <th>שם הצבע</th>
                    <th>מחיר</th>
                    <th>סדר הצגה</th>
                    <th>במלאי</th>
                </tr>
            </thead>
            <tbody id="colorsTableBody">
                
            </tbody>
        </table>

    </div>

    <form method="post" asp-action="AddColorWithPicker">
        <h2>ערכים</h2>
        <label>id: </label> <label type="number" id="idColor" name="idColor" />
        <label>אינדקס הצגה: </label> <label type="number" id="order" name="order" />
        <label for="colorName">שם הצבע:</label>
        <input type="text" id="colorName" name="colorName" placeholder="הכנס שם הצבע" />

        <label for="colorPicker">הצבע:</label>
        <input type="color" id="colorPicker" name="colorHex" value="#ff0000" />

        <br />
        <label for="price">מחיר:</label>
        <input type="number" id="price" name="price" />
        <br />
        <label for="inStock">במלאי:</label>
        <input type="checkbox" id="inStock" name="inStock" />
        <br />
        <button onclick="Edit()" id="editButton" disabled>ערוך צבע</button>
        <button onclick="AddColor()" type="submit"> צבע חדש</button>
    </form>

</main>
<script>

</script>