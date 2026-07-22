function formatDate(value) {
  if (!value) return "-";
  const date = new Date(value);
  return date.toLocaleDateString(undefined, {
    year: "numeric",
    month: "short",
    day: "numeric",
  });
}

function StudentList({ students, onEdit, onDelete, loading }) {
  if (loading) {
    return <div className="status-message">Loading students...</div>;
  }

  if (!students || students.length === 0) {
    return <div className="status-message">No students available. Add a student to begin.</div>;
  }

  return (
    <div className="table-card">
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Age</th>
            <th>Course</th>
            <th>Created Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.id}>
              <td>{student.id}</td>
              <td>{student.name}</td>
              <td>{student.email}</td>
              <td>{student.age}</td>
              <td>{student.course}</td>
              <td>{formatDate(student.createdDate)}</td>
              <td className="actions-cell">
                <button className="action-button" onClick={() => onEdit(student)}>
                  Edit
                </button>
                <button className="action-button danger" onClick={() => onDelete(student.id)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default StudentList;
