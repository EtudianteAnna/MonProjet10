document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".patient-row").forEach(function (row) {
        row.addEventListener("click", function () {
            const patientId = this.dataset.patientId;
            fetchNotes(patientId);
        });
    });

    document.getElementById("addNoteBtn").addEventListener("click", function () {
        document.getElementById("addNoteModal").style.display = "block";
    });

    document.getElementById("addNoteForm").addEventListener("submit", function (event) {
        event.preventDefault();
        const patientId = document.querySelector(".patient-row.selected").dataset.patientId;
        const noteContent = document.getElementById("noteContent").value;

        const note = {
            PatientId: patientId,
            Content: noteContent,
            CreatedDate: new Date().toISOString()
        };

        fetch('http://localhost:5106/api/ApiGateway/notes', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(note)
        }).then(response => {
            if (response.ok) {
                document.getElementById("addNoteModal").style.display = "none";
                fetchNotes(patientId);
            } else {
                alert("Failed to add note.");
            }
        });
    });
});

function fetchNotes(patientId) {
    fetch(`http://localhost:5106/api/ApiGateway/notes/${patientId}`)
        .then(response => response.json())
        .then(data => {
            const notesTableBody = document.querySelector("#notesTable tbody");
            notesTableBody.innerHTML = "";
            data.forEach(note => {
                const row = document.createElement("tr");
                const contentCell = document.createElement("td");
                contentCell.textContent = note.content;
                const dateCell = document.createElement("td");
                dateCell.textContent = new Date(note.createdDate).toLocaleString();
                row.appendChild(contentCell);
                row.appendChild(dateCell);
                notesTableBody.appendChild(row);
            });
            document.getElementById("notesSection").style.display = "block";
        });
}
