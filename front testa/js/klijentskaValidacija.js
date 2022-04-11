function validacija() {
  let name = document.getElementById('inputName').value;
  let band = document.getElementById('selektBand').value;
  let genre = document.getElementById('inputGenre').value;
  let year = document.getElementById('inputYear').value;
  let sold = document.getElementById('inputSold').value;

  if (!name) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  if (!genre) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  if (!year) {
    alert('greska prilikom dodavanja!');
    return false;
  }
  if (!sold) {
    alert('greska prilikom dodavanja!');
    return false;
  }
  if (!band) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  let brojRegex = /^[0-9]*$/;

  if (!brojRegex.test(year)) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  if (!brojRegex.test(sold)) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  if (year < 1950 || year > 2022) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  if (sold < 0 || sold > 100) {
    alert('greska prilikom dodavanja!');
    return false;
  }

  return true;
}
function validacijaFilter() {
  let min = document.getElementById('minYear').value;

  let max = document.getElementById('maxYear').value;
  if (!min) {
    alert('Greska prilikom pretrage!');
    return false;
  }
  if (!max) {
    alert('Greska prilikom pretrage!');
    return false;
  }
  if (min < 1950 || min > 2022) {
    alert('Greska prilikom pretrage!');
    return false;
  }
  if (max < min) {
    alert('Greska prilikom pretrage!');
    return false;
  }

  if (max < 1950 || max > 2022) {
    alert('Greska prilikom pretrage!');
    return false;
  }
  let brojRegex = /^[0-9]*$/;
  if (!brojRegex.test(max)) {
    alert('Greska prilikom pretrage!');
    return false;
  }

  if (!brojRegex.test(min)) {
    alert('Greska prilikom pretrage!');
    return false;
  }

  return true;
}
