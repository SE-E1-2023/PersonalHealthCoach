const userId = "39026d47-164d-4eb8-a026-4aed4cb7043";
const url = `http://localhost:7071/api/v1/users/${userId}/data/personal/`;
const fileName = 'weight.json';

fetch(url)
  .then(response => response.json())
  .then(data => {
    // Filtrăm datele pentru ultimele 7 zile
    const lastSevenDaysData = data.filter(item => {
      const date = new Date(item.date);
      const now = new Date();
      const daysDifference = Math.round((now - date) / (1000 * 60 * 60 * 24));
      return daysDifference <= 7;
    });

    // Transformăm datele în formatul dorit pentru a le adăuga la fișierul JSON
    const newData = lastSevenDaysData.map(item => {
      return { label: item.date, value: item.weight };
    });

    // Citim fișierul JSON
    fetch(fileName)
      .then(response => response.json())
      .then(json => {
        // Adăugăm datele noi la fișierul JSON existent
        const updatedData = json.concat(newData);

        // Scriem datele actualizate înapoi în fișierul JSON
        const updatedJson = JSON.stringify(updatedData, null, 2);
        const blob = new Blob([updatedJson], { type: 'application/json' });
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.download = fileName;
        link.href = url;
        link.click();
      })
      .catch(error => {
        console.error(error);
      });
  })
  .catch(error => {
    console.error(error);
  });
