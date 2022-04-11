// podaci od interesa
var host = 'https://localhost:';
var port = '44396/';
var albumsEndpoint = 'api/albumi/';
var bandsEndpoint = 'api/bendovi/';
var loginEndpoint = 'api/authentication/login';
var registerEndpoint = 'api/authentication/register';
var formAction = 'Create';
var editingId;
var jwt_token;

// prikaz forme za prijavu
function showLogin() {
  document.getElementById('usernameLogin').value = '';
  document.getElementById('passwordLogin').value = '';
  document.getElementById('loginForm').style.display = 'block';
  document.getElementById('registerForm').style.display = 'none';
  document.getElementById('logout').style.display = 'none';
  document.getElementById('pocetna-dugmad').style.display = 'none';
}
function initialState() {
  document.getElementById('logout').style.display = 'none';
  document.getElementById('pocetna-dugmad').style.display = 'block';
  document.getElementById('loginForm').style.display = 'none';
  document.getElementById('registerForm').style.display = 'none';
  document.getElementById('data-onload').style.display = 'none';
  document.getElementById('addNewAlbum').style.display = 'none';
  loadAlbums();
}

function validateRegisterForm(username, email, password, confirmPassword) {
  if (username.length === 0) {
    alert('Username field can not be empty.');
    return false;
  } else if (email.length === 0) {
    alert('Email field can not be empty.');
    return false;
  } else if (password.length === 0) {
    alert('Password field can not be empty.');
    return false;
  } else if (confirmPassword.length === 0) {
    alert('Confirm password field can not be empty.');
    return false;
  } else if (password !== confirmPassword) {
    alert('Password value and confirm password value should match.');
    return false;
  }
  return true;
}

function registerUser() {
  var username = document.getElementById('usernameRegister').value;
  var email = document.getElementById('emailRegister').value;
  var password = document.getElementById('passwordRegister').value;
  var confirmPassword = document.getElementById(
    'confirmPasswordRegister'
  ).value;

  if (validateRegisterForm(username, email, password, confirmPassword)) {
    var url = host + port + registerEndpoint;
    var sendData = { Username: username, Email: email, Password: password };
    fetch(url, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(sendData),
    })
      .then((response) => {
        if (response.status === 200) {
          console.log('Successful registration');
          username = '';
          email = '';
          password = '';
          confirmPassword = '';
          alert('Successful registration');
          showLogin();
        } else {
          console.log('Error occured with code ' + response.status);
          console.log(response);
          alert('Greska prilikom registracije!');
        }
      })
      .catch((error) => console.log(error));
  }
  return false;
}

// prikaz forme za registraciju
function showRegistration() {
  document.getElementById('usernameRegister').value = '';
  document.getElementById('emailRegister').value = '';
  document.getElementById('passwordRegister').value = '';
  document.getElementById('confirmPasswordRegister').value = '';
  document.getElementById('loginForm').style.display = 'none';
  document.getElementById('pocetna-dugmad').style.display = 'none';
  document.getElementById('registerForm').style.display = 'block';
  document.getElementById('logout').style.display = 'none';
}

function validateLoginForm(username, password) {
  if (username.length === 0) {
    alert('Username field can not be empty.');
    return false;
  } else if (password.length === 0) {
    alert('Password field can not be empty.');
    return false;
  }
  return true;
}

function loginUser() {
  var username = document.getElementById('usernameLogin').value;
  var password = document.getElementById('passwordLogin').value;

  if (validateLoginForm(username, password)) {
    var url = host + port + loginEndpoint;
    var sendData = { Username: username, Password: password };
    fetch(url, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(sendData),
    })
      .then((response) => {
        if (response.status === 200) {
          console.log('Successful login');
          alert('Successful login');
          response.json().then(function (data) {
            console.log(data);
            document.getElementById('info').innerHTML =
              'Prijavljeni korisnik: <b>' + data.email + '<b/>.';
            document.getElementById('logout').style.display = 'block';
            document.getElementById('addNewAlbum').style.display = 'block';
            document.getElementById('loginForm').style.display = 'none';
            jwt_token = data.token;
            loadBands();
            loadAlbums();
          });
        } else {
          console.log('Error occured with code ' + response.status);
          console.log(response);
          alert('Greska prilikom prijave!');
        }
      })
      .catch((error) => console.log(error));
  }
  return false;
}

// prikaz albuma
function loadAlbums() {
  document.getElementById('data-onload').style.display = 'block';
  document.getElementById('usernameLogin').value = '';
  document.getElementById('passwordLogin').value = '';

  // ucitavanje albuma
  var requestUrl = host + port + albumsEndpoint;
  console.log('URL zahteva: ' + requestUrl);
  var headers = {};
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  }
  console.log(headers);
  fetch(requestUrl, { headers: headers })
    .then((response) => {
      if (response.status === 200) {
        response.json().then(setAlbums);
      } else {
        console.log('Error occured with code ' + response.status);
        showError();
      }
    })
    .catch((error) => console.log(error));
}

function showError() {
  var container = document.getElementById('data-onload');
  container.innerHTML = '';

  var div = document.createElement('div');
  var h1 = document.createElement('h1');
  var errorText = document.createTextNode(
    'Greska prilikom preuzimanja Albuma!'
  );

  h1.appendChild(errorText);
  div.appendChild(h1);
  container.append(div);
}

// metoda za postavljanje albuma u tabelu
function setAlbums(data) {
  var container = document.getElementById('data-onload');
  container.innerHTML = '';

  var naslov = document.createElement('h1');
  var textna = document.createTextNode('Albumi');
  naslov.appendChild(textna);
  container.appendChild(naslov);

  console.log(data);

  var table = document.createElement('table');
  // table.classList.add('table', 'table-bordered');
  table.setAttribute('id', 'albumi');
  var header = createHeader();
  table.append(header);

  var tableBody = document.createElement('tbody');

  for (var i = 0; i < data.length; i++) {
    // prikazujemo novi red u tabeli
    var row = document.createElement('tr');
    // prikaz podataka
    row.appendChild(createTableCell(data[i].name));
    row.appendChild(createTableCell(data[i].year));
    row.appendChild(createTableCell(data[i].genre));
    row.appendChild(createTableCell(data[i].band));
    if (jwt_token) {
      row.appendChild(createTableCell(data[i].sold));
      // prikaz dugmadi za izmenu i brisanje
      var stringId = data[i].id.toString();

      var buttonDelete = document.createElement('button');
      buttonDelete.name = stringId;
      buttonDelete.setAttribute('class', 'btn btn-danger');
      buttonDelete.addEventListener('click', deleteAlbum);
      var buttonDeleteText = document.createTextNode('Obriši');
      buttonDelete.appendChild(buttonDeleteText);
      var buttonDeleteCell = document.createElement('td');
      buttonDeleteCell.appendChild(buttonDelete);
      row.appendChild(buttonDeleteCell);

      var buttonEdit = document.createElement('button');
      buttonEdit.name = stringId;
      buttonEdit.setAttribute('class', 'btn btn-warning');
      buttonEdit.addEventListener('click', editAlbum);
      var buttonEditText = document.createTextNode('Izmeni');
      buttonEdit.appendChild(buttonEditText);
      var buttonEditCell = document.createElement('td');
      buttonEditCell.appendChild(buttonEdit);
      row.appendChild(buttonEditCell);
    }

    tableBody.appendChild(row);
  }
  table.appendChild(tableBody);
  container.appendChild(table);

  clearAlbumForm();
}

function createHeader() {
  var thead = document.createElement('thead');
  var row = document.createElement('tr');

  row.appendChild(createTableCell('Ime'));
  row.appendChild(createTableCell('Godina izdavanja'));
  row.appendChild(createTableCell('Zanr'));
  row.appendChild(createTableCell('Bend'));
  if (jwt_token) {
    row.appendChild(createTableCell('Broj prodatih primeraka (M)'));
    row.appendChild(createTableCell('Obrisi'));
    row.appendChild(createTableCell('Izmeni'));
  }
  row.style.backgroundColor = '#FFA500';
  thead.appendChild(row);
  return thead;
}

function createTableCell(text) {
  var cell = document.createElement('td');
  var cellText = document.createTextNode(text);
  cell.appendChild(cellText);
  return cell;
}
function createTableCellHeader(text) {
  var cell = document.createElement('th');
  var cellText = document.createTextNode(text);
  cell.appendChild(cellText);
  return cell;
}

// dodavanje novog albuma
function submitAlbumForm() {
  var inputName = document.getElementById('inputName').value;
  var inputGenre = document.getElementById('inputGenre').value;
  var inputYear = document.getElementById('inputYear').value;
  var inputSold = document.getElementById('inputSold').value;
  var inputBand = document.getElementById('selektBand').value;
  var httpAction;
  var sendData;
  var url;

  // u zavisnosti od akcije pripremam objekat
  if (formAction === 'Create') {
    httpAction = 'POST';
    url = host + port + albumsEndpoint;
    sendData = {
      Name: inputName,
      Genre: inputGenre,
      Year: inputYear,
      Sold: inputSold,
      BandId: inputBand,
    };
  } else {
    httpAction = 'PUT';
    url = host + port + albumsEndpoint + editingId.toString();
    sendData = {
      Id: editingId,
      Name: inputName,
      Genre: inputGenre,
      Year: inputYear,
      Sold: inputSold,
      BandId: inputBand,
    };
  }
  console.log(url);
  console.log('Objekat za slanje');
  console.log(sendData);
  var headers = { 'Content-Type': 'application/json' };
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  } else {
    alert('Morate se ulogovati da bi dodali novu sliku!');
    clearAlbumForm();
    return false;
  }
  fetch(url, {
    method: httpAction,
    headers: headers,
    body: JSON.stringify(sendData),
  })
    .then((response) => {
      if (response.status === 201 || response.status === 200) {
        console.log('Successful action');
        refreshTable();
      } else {
        console.log('Error occured with code ' + response.status);
        alert('Greska prilikom dodavanja!');
      }
    })
    .catch((error) => console.log(error));
  return false;
}

// brisanje albuma
function deleteAlbum() {
  // izvlacimo {id}
  var deleteID = this.name;
  // saljemo zahtev
  var url = host + port + albumsEndpoint + deleteID.toString();
  var headers = { 'Content-Type': 'application/json' };
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  }
  fetch(url, { method: 'DELETE', headers: headers })
    .then((response) => {
      if (response.status === 204) {
        console.log('Successful action');
        refreshTable();
      } else {
        console.log('Error occured with code ' + response.status);
        alert('Desila se greska prilikom brisanja!');
      }
    })
    .catch((error) => console.log(error));
}

// osvezi prikaz tabele
function refreshTable() {
  // cistim formu
  document.getElementById('minYear').value = '';
  document.getElementById('maxYear').value = '';
  clearAlbumForm();
  loadAlbums();
}

function clearAlbumForm() {
  document.getElementById('inputName').value = '';
  document.getElementById('inputGenre').value = '';
  document.getElementById('inputYear').value = '';
  document.getElementById('inputSold').value = '';
  // document.getElementById('minYear').value = '';
  // document.getElementById('maxYear').value = '';
  document.getElementById('selektBand').value = '2';
  document.getElementById('dodavanjeAlbuma').style.display = 'block';
  document.getElementById('izmenaAlbuma').style.display = 'none';
  formAction = 'Create';
}

function logout() {
  jwt_token = undefined;
  document.getElementById('info').innerHTML = '';
  initialState();
  loadAlbums();
}

function loadBands() {
  var select = document.getElementById('selektBand');
  // ucitavanje benda
  var requestUrl = host + port + bandsEndpoint;
  console.log('URL zahteva: ' + requestUrl);
  var headers = {};
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  }
  console.log(headers);
  fetch(requestUrl)
    .then((response) => {
      if (response.status === 200) {
        response.json().then((data) => {
          select.innerHTML = '';
          for (var i = 0; i < data.length; i++) {
            var option = document.createElement('option');
            option.setAttribute('value', data[i].id);
            var text = document.createTextNode(data[i].name);
            option.appendChild(text);
            select.appendChild(option);
          }
        });
      } else {
        console.log('Error occured with code ' + response.status);
        showError();
      }
    })
    .catch((error) => console.log(error));
}

function filterByYear() {
  var minYear = document.getElementById('minYear').value;
  var maxYear = document.getElementById('maxYear').value;
  var httpAction;
  var sendData;
  var url;

  httpAction = 'POST';
  url = host + port + 'api/pretraga';
  sendData = {
    min: minYear,
    max: maxYear,
  };
  console.log(url);
  console.log('Objekat za slanje filtera');
  console.log(sendData);
  var headers = { 'Content-Type': 'application/json' };
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  } else {
    alert('Morate se registrovati da bi filtrirali');
    clearAlbumForm();
    return false;
  }
  fetch(url, {
    method: httpAction,
    headers: headers,
    body: JSON.stringify(sendData),
  })
    .then((response) => {
      if (response.status === 200) {
        console.log('Successful action');
        response.json().then(setAlbums);
      } else {
        console.log('Error occured with code ' + response.status);
        alert('Greska prilikom pretrage! (server)');
      }
    })
    .catch((error) => console.log(error));
  return false;
}

function editAlbum() {
  // izvlacimo id
  var editId = this.name;

  var url = host + port + albumsEndpoint + editId.toString();
  var headers = {};
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  }
  fetch(url, { headers: headers })
    .then((response) => {
      if (response.status === 200) {
        console.log('Successful action');
        response.json().then((data) => {
          console.log(data);
          document.getElementById('dodavanjeAlbuma').style.display = 'none';
          document.getElementById('izmenaAlbuma').style.display = 'block';
          document.getElementById('inputName').value = data.name;
          document.getElementById('inputYear').value = data.year;
          document.getElementById('inputGenre').value = data.genre;
          document.getElementById('inputSold').value = data.sold;
          findBandByName(data.band);
          // document.getElementById('selektBand').value = data.band;
          editingId = data.id;
          formAction = 'Update';
        });
      } else {
        formAction = 'Create';
        console.log('Error occured with code ' + response.status);
        alert('Desila se greska!');
      }
    })
    .catch((error) => console.log(error));
}

function findBandByName(band) {
  var url2 = host + port + 'api/bendovi/trazi?naziv=' + band;
  var headers = {};
  if (jwt_token) {
    headers.Authorization = 'Bearer ' + jwt_token; // headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
  }
  fetch(url2, { headers: headers })
    .then((response) => {
      if (response.status === 200) {
        response.json().then((data) => {
          console.log('podaci iz find by name: ');
          console.log(data);
          document.getElementById('selektBand').value = data[0].id;
        });
      } else {
        console.log('Error occured with code ' + response.status);
        alert('Desila se greska!');
      }
    })
    .catch((error) => console.log(error));
}
