import { useEffect, useState } from "react";

function StudentForm({ mode, initialData, onCancel, onSubmit, loading }) {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [age, setAge] = useState("");
  const [course, setCourse] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    if (initialData) {
      setName(initialData.name || "");
      setEmail(initialData.email || "");
      setAge(initialData.age?.toString() || "");
      setCourse(initialData.course || "");
      setError("");
    } else {
      setName("");
      setEmail("");
      setAge("");
      setCourse("");
      setError("");
    }
  }, [initialData]);

  const handleFormSubmit = (event) => {
    event.preventDefault();
    setError("");

    if (!name.trim() || !email.trim() || !course.trim()) {
      setError("All fields are required.");
      return;
    }

    const ageValue = Number(age);
    if (!age || Number.isNaN(ageValue) || ageValue < 1) {
      setError("Please enter a valid age.");
      return;
    }

    onSubmit({
      name: name.trim(),
      email: email.trim(),
      age: ageValue,
      course: course.trim(),
    });
  };

  return (
    <div className="card form-card">
      <div className="form-header">
        <div>
          <h2>{mode === "edit" ? "Edit Student" : "Add Student"}</h2>
          <p className="subtitle">
            {mode === "edit"
              ? "Update the student details below."
              : "Create a new student record."}
          </p>
        </div>
        <button type="button" className="text-button" onClick={onCancel}>
          Cancel
        </button>
      </div>

      <form onSubmit={handleFormSubmit}>
        <div className="form-grid">
          <label>
            Name
            <input value={name} onChange={(e) => setName(e.target.value)} />
          </label>
          <label>
            Email
            <input value={email} onChange={(e) => setEmail(e.target.value)} />
          </label>
          <label>
            Age
            <input
              type="number"
              min="1"
              value={age}
              onChange={(e) => setAge(e.target.value)}
            />
          </label>
          <label>
            Course
            <input value={course} onChange={(e) => setCourse(e.target.value)} />
          </label>
        </div>

        {error && <div className="message error">{error}</div>}

        <div className="form-actions">
          <button type="submit" className="primary-button" disabled={loading}>
            {loading ? "Saving..." : mode === "edit" ? "Save Changes" : "Create Student"}
          </button>
          <button
            type="button"
            className="secondary-button"
            onClick={onCancel}
            disabled={loading}
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  );
}

export default StudentForm;
