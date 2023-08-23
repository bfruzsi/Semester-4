let cocktails = [];
let connection = null;
let cocktailIdToUpdate = -1;
let avgprice = null;
let rendered = false;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:61344/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CocktailDeleted", (user, message) => {
        getdata();
    });

    connection.on("CocktailCreated", (user, message) => {
        getdata();
    });

    connection.on("CocktailUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

async function getdata() {
    await fetch('http://localhost:61344/cocktail')
        .then(x => x.json())
        .then(y => {
            cocktails = y;
            console.log(cocktails)
            display();
        });

    await fetch('http://localhost:61344/stat/AVGPrice')
        .then(x => x.json())
        .then(y => {
            avgprice = y;
            display2();
        })
}

function display() {
    document.getElementById('results').innerHTML = "";
    cocktails.forEach(z => {
        document.getElementById('results').innerHTML += "<tr><td>" + z.cocktailId
            + "</td><td>" + z.cocktailName + "</td><td>" + z.price + "</td><td>" +
            `<button type="button" onclick="remove(${z.cocktailId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${z.cocktailId})">Update</button>`
            + "</td></tr>";
        console.log(z.cocktailName);
    });
}

function display2() {
    document.getElementById('noncrudform').innerHTML = "";
    document.getElementById('noncrudform').innerHTML += "<p>Average Price for Cocktails" + "<br/>" + avgprice[0].value + " ";
}

function create() {
    let cocktailName = document.getElementById('cocktailname').value;
    let price = document.getElementById('price').value;
    fetch('http://localhost:61344/cocktail', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { cocktailName: cocktailName, price: price }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function showupdate(id) {
    document.getElementById('cocktailnametoupdate').value = cocktails.find(t => t['cocktailId'] == id)['cocktailName'];
    document.getElementById('pricetoupdate').value = cocktails.find(t => t['cocktailId'] == id)['price'];
    document.getElementById('updateform').style.display = 'inline';
    cocktailIdToUpdate = id;
}

function update() {
    document.getElementById('updateform').style.display = 'none';
    let cocktailname = document.getElementById('cocktailnametoupdate').value;
    let price = document.getElementById('pricetoupdate').value;
    fetch('http://localhost:61344/cocktail', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { cocktailName: cocktailname, cocktailId: cocktailIdToUpdate, price: price }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:61344/cocktail/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}