import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import StudentList from "../components/StudentList";
import StudentForm from "../components/StudentForm";
import {
  fetchStudents,
  addStudent,
  updateStudent,
  deleteStudent,
  removeToken,
} from "../services/api";

function Students() {
  const navigate = useNavigate();
  const [students, setStudents] = useState([]);
  const [pageLoading, setPageLoading] = useState(true);
  const [formLoading, setFormLoading] = useState(false);
  const [error, setError] = useState("");
  const [feedback, setFeedback] = useState("");
  const [selectedStudent, setSelectedStudent] = useState(null);
  const [formMode, setFormMode] = useState("add");
  const [formVisible, setFormVisible] = useState(false);

  const loadStudents = async () => {
    setError("");
    setPageLoading(true);
    try {
      const response = await fetchStudents();
      setStudents(response.data || []);
    } catch (error) {
      if (error?.response?.status === 401) {
        return;
      }
      setError(
        "Unable to load students. Please check the backend or try again later."
      );
    } finally {
      setPageLoading(false);
    }
  };

  useEffect(() => {
    loadStudents();
  }, []);

  const handleLogout = () => {
    removeToken();
    navigate("/login", { replace: true });
  };

  const handleAddClick = () => {
    setFormMode("add");
    setSelectedStudent(null);
    setFeedback("");
    setError("");
    setFormVisible(true);
  };

  const handleEdit = (student) => {
    setFormMode("edit");
    setSelectedStudent(student);
    setFeedback("");
    setError("");
    setFormVisible(true);
  };

  const handleFormCancel = () => {
    setFormVisible(false);
    setSelectedStudent(null);
    setError("");
  };

  const showFeedback = (message) => {
    setFeedback(message);
    setTimeout(() => setFeedback(""), 4500);
  };

  const handleFormSubmit = async (studentData) => {
    setError("");
    setFormLoading(true);
    try {
      if (formMode === "add") {
        await addStudent(studentData);
        showFeedback("Student added successfully.");
      } else {
        await updateStudent(selectedStudent.id, studentData);
        showFeedback("Student updated successfully.");
      }
      setFormVisible(false);
      setSelectedStudent(null);
      await loadStudents();
    } catch (error) {
      if (error?.response?.status === 400) {
        setError("Please review the student details and try again.");
      } else if (error?.response?.status === 404) {
        setError("Student record was not found. Please refresh the list.");
      } else {
        setError("Unable to save student. Please try again later.");
      }
    } finally {
      setFormLoading(false);
    }
  };

  const handleDelete = async (studentId) => {
    const confirmed = window.confirm(
      "Are you sure you want to delete this student?"
    );
    if (!confirmed) {
      return;
    }

    setError("");
    setFormLoading(true);
    try {
      await deleteStudent(studentId);
      showFeedback("Student deleted successfully.");
      await loadStudents();
    } catch (error) {
      if (error?.response?.status === 404) {
        setError("Student not found. It may already have been removed.");
      } else {
        setError("Unable to delete student. Please try again later.");
      }
    } finally {
      setFormLoading(false);
    }
  };

  return (
    <div className="page-shell">
      <header className="page-header">
        <div>
          <p className="eyebrow">Student Management</p>
          <h1>Manage Students</h1>
          <p className="subtitle">
          </p>
        </div>

        <div className="header-actions">
          <button className="secondary-button" onClick={handleAddClick}>
            Add Student
          </button>
          <button className="secondary-button" onClick={handleLogout}>
            Logout
          </button>
        </div>
      </header>

      {error && <div className="message error">{error}</div>}
      {feedback && <div className="message success">{feedback}</div>}

      {formVisible && (
        <StudentForm
          mode={formMode}
          initialData={selectedStudent}
          onCancel={handleFormCancel}
          onSubmit={handleFormSubmit}
          loading={formLoading}
        />
      )}

      <StudentList
        students={students}
        onEdit={handleEdit}
        onDelete={handleDelete}
        loading={pageLoading}
      />
    </div>
  );
}

export default Students;
