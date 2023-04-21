const authorsSelect = document.getElementById("form-author");
const genresSelect = document.getElementById("form-genres");
const hasVideoclip = document.getElementById("hasVideoclip");
const videoForm = document.getElementById("video-clip");
const btnSubmit = document.getElementById("btn-submit");


const baseUrl = "https://localhost:7286"

const createOptionForSelect = (item) => `<option value='${item.value}'>${item.label}</option>`;

fetch(baseUrl + '/authors')
  .then(response => response.json())
  .then(authors => authors.forEach(item => {
    authorsSelect.innerHTML+=createOptionForSelect(item)
  }));

fetch(baseUrl + '/genres')
  .then(response => response.json())
  .then(genres => genres.forEach(item => {
    genresSelect.innerHTML+=createOptionForSelect(item)
  })); 

hasVideoclip.addEventListener("change", (e) => {
    videoForm.style.display = e.target.checked ? "block" : "none"
})

btnSubmit.addEventListener("click", async (e) => {
  e.preventDefault();

  const form = document.forms.musicForm;

  const body = {
    Name: form.musicName.value,
    FilePath: form.musicFilePath.value,
    ReleaseDate: form.musicRelease.value,
    Author: form.author.value,
    Genre: form.genre.value,
    VideoClip: hasVideoclip.checked 
      ? {
        FilePath:form.videoFilePath.value,
        ReleaseDate: form.videoRelease.value,
        DirectBy: form.videoDirectBy.value
      } 
      : null
  }
  const response = await fetch(baseUrl+"/music", {
    method: "POST",
    body: JSON.stringify(body),
    headers: {
      "Content-Type":"application/json"
    }
  });

  if (response.ok) {
    Alert("Отправлено!")
  }
});

