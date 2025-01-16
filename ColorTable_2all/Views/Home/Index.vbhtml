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
                    <th>id</th>
                    <th>שם הצבע</th>
                    <th>מחיר</th>
                    <th>סדר הצגה</th>
                    <th>במלאי</th>
                    <th>קוד הצבע</th>
                    <th></th>
                </tr>
            </thead>
            <tbody id="colorsTableBody">
            </tbody>
        </table>

    </div>

    <form method="post" asp-action="AddColorWithPicker">
        <h2>ערכים</h2>

        <p dir="rtl" id="idColorLabel">ID: <span id="idColor"></span></p>
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
        <label>אינדקס הצגה: </label>
        <input type="number" id="order" name="order" min="1" />
        <br />
        <button type="button" onclick="editColor()" id="editButton" disabled>ערוך צבע</button>
        <button type="button" onclick="AddColor()"> צבע חדש</button>
    </form>



</main>
<script>


    document.addEventListener('DOMContentLoaded', function () {
        loadColors();
    });

    function loadColors() {
        fetch('/Colors/GetColors')
            .then(response => response.json())
            .then(data => {
                console.log(data);
                let tableBody = document.getElementById('colorsTableBody');
                tableBody.innerHTML = ''; 
                data.sort((color1, color2) => color1.Order - color2.Order).forEach(function (color) {
                    let row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${color.Id}</td>
                        <td>${color.Name}</td>
                        <td>${color.Price}</td>
                        <td>${color.Order}</td>
                        <td>${color.InStock ? 'כן' : 'לא'}</td>
                        <td style="background-color: ${color.Code};">${color.Code}</td>
                        <td>
                            <button onclick="selectToEditColor(${color.Id})"><i class="fas fa-edit"></i> ערוך</button>
                            <button onclick="deleteColor(${color.Id})"><i class="fas fa-trash"></i>מחק</button>
                        </td>
                    `;
                    tableBody.appendChild(row);
                });
            })
            .catch(error => console.error('Error:', error));
    }


    function AddColor() {
        const newColor = {
            Name: document.getElementById('colorName').value,
            Price: document.getElementById('price').value,
            Order: document.getElementById('order').value,
            InStock: document.getElementById('inStock').checked,
            Code: document.getElementById('colorPicker').value
        };

        fetch('/Colors/AddColor', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newColor)
        }).then(response => {
            if (response.ok) {
                loadColors();
            } else {
                console.error('Failed to add color.');
            }
        })
        .catch(error => console.error('Error:', error));
    }


    function selectToEditColor(id) {
        fetch(`/Colors/GetColorById/${id}`)
            .then(response => response.json())
            .then(color => {
                document.getElementById('colorName').value = color.Name;
                document.getElementById('colorPicker').value = color.Code;
                document.getElementById('price').value = color.Price;
                document.getElementById('order').value = color.Order;
                document.getElementById('inStock').checked = color.InStock;
                document.getElementById('idColor').textContent = color.Id;

                document.getElementById('editButton').disabled = false;
            })
            .catch(error => console.error('Error:', error));
    }

    function editColor() {
        const updatedColor = {
            Id: document.getElementById('idColor').textContent,
            Name: document.getElementById('colorName').value,
            Code: document.getElementById('colorPicker').value,
            Price: document.getElementById('price').value,
            Order: document.getElementById('order').value,
            InStock: document.getElementById('inStock').checked
        };

        fetch('/Colors/EditColor', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedColor)
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('שגיאה: ' + data.message); 
                    loadColors(); 
                } else {
                    alert('שגיאה: ' + data.message); 
                }
            })
            .catch(error => console.error('Error:', error));

        document.getElementById('editButton').disabled = true;
    }



    function deleteColor(id) {
        fetch('/Colors/DeleteColor', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: id }) 
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    loadColors();
                } else {
                    alert('מחיקת הצבע נכשלה.');
                }
            })
            .catch(error => console.error('Error:', error));
    }

</script>
