import React, { useEffect, useState } from "react";
import SidebarLayout from './SideBarCLayout/SideBarCLayout';
import axios from "axios";
import './CandidateForm.css';

const Candidates = () => {
    const [candidates, setCandidates] = useState([]);
    const [selectedCandidate, setSelectedCandidate] = useState(null);
    const [showForm, setShowForm] = useState(false);
    const [formData, setFormData] = useState({
        username: "",
        file_Cv: null,
        position: "",
        review: 0
    });
    const [error, setError] = useState("");
    const [cvUrl, setCvUrl] = useState(null);
    const [showCvModal, setShowCvModal] = useState(false);

    useEffect(() => {
        getCandidates();
    }, []);

    const getCandidates = async () => {
        try {
            const res = await axios.get("https://localhost:7159/Candidate/List");
            if (Array.isArray(res.data)) {
                setCandidates(res.data);
                setError("");
            } else {
                setCandidates([]);
                setError("Unexpected response format.");
            }
        } catch (error) {
            setError("Failed to load candidate list. Server may be unavailable.");
        }
    };

    const handleAddClick = () => {
        setSelectedCandidate(null);
        setFormData({
            username: "",
            file_Cv: null,
            position: "",
            review: 0
        });
        setShowForm(true);
        setError("");
    };

    const handleEdit = (candidate) => {
        setSelectedCandidate(candidate);
        setFormData({
            username: candidate.username,
            file_Cv: null,
            position: candidate.position,
            review: candidate.review
        });
        setShowForm(true);
        setError("");
    };

    const handleDelete = async (username) => {
        if (!window.confirm("Are you sure you want to delete this candidate?")) return;
        try {
            await axios.delete(`https://localhost:7159/Candidate/Delete/${username}`);
            getCandidates();
        } catch (error) {
            setError("Failed to delete candidate.");
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;

        if (name === "review") {
            const numericValue = parseInt(value, 10);
            if (numericValue < 1 || numericValue > 5) return;
            setFormData((prev) => ({ ...prev, [name]: numericValue }));
            return;
        }

        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file && file.type !== "application/pdf") {
            setError("Only PDF files are allowed.");
            return;
        }
        setFormData((prev) => ({ ...prev, file_Cv: file }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");

        try {
            const data = new FormData();
            data.append("Username", formData.username);
            data.append("Position", formData.position);
            data.append("Review", formData.review);
            if (formData.file_Cv) {
                data.append("cv", formData.file_Cv);
            }

            if (selectedCandidate) {
                await axios.put(`https://localhost:7159/Candidate/Update/${formData.username}`, {
                    username: formData.username,
                    position: formData.position,
                    review: formData.review,
                    recrComp: ""
                });
            } else {
                await axios.post("https://localhost:7159/Candidate/Create", data, {
                    headers: { "Content-Type": "multipart/form-data" }
                });
            }

            getCandidates();
            setShowForm(false);
            setSelectedCandidate(null);
            setFormData({
                username: "",
                file_Cv: null,
                position: "",
                review: 0
            });
        } catch (err) {
            console.error("Error saving candidate:", err);
            setError("An error occurred while saving the candidate. Please try again.");
        }
    };

    return (
        <SidebarLayout>
            <div className="candidate-page">
                {!showForm && (
                    <button className="btn add-btn" onClick={handleAddClick}>
                        Add Candidate
                    </button>
                )}

                {showForm && (
                    <form onSubmit={handleSubmit} className="form-box login">
                        <h1>{selectedCandidate ? "Edit Candidate" : "Add Candidate"}</h1>

                        {error && <div className="error">{error}</div>}

                        <div className="input-group">
                            <label>Username</label>
                            <input
                                type="text"
                                placeholder="Enter username"
                                name="username"
                                value={formData.username}
                                onChange={handleChange}
                                disabled={!!selectedCandidate}
                                required
                            />
                        </div>

                        <div className="input-group">
                            <label>Upload CV (PDF only)</label>
                            <input
                                type="file"
                                name="file_Cv"
                                accept=".pdf"
                                onChange={handleFileChange}
                                required={!selectedCandidate}
                            />
                        </div>

                        <div className="input-group">
                            <label>Position</label>
                            <input
                                type="text"
                                placeholder="Enter position"
                                name="position"
                                value={formData.position}
                                onChange={handleChange}
                                required
                            />
                        </div>

                        <div className="input-group">
                            <label>Review (1 to 5)</label>
                            <input
                                type="number"
                                name="review"
                                min="1"
                                max="5"
                                step="1"
                                value={formData.review}
                                onChange={handleChange}
                                required
                            />
                        </div>

                        <div className="button-group">
                            <button type="submit" className="btn">
                                {selectedCandidate ? "Update" : "Save"}
                            </button>
                            <button
                                type="button"
                                className="btn cancel"
                                onClick={() => {
                                    setShowForm(false);
                                    setSelectedCandidate(null);
                                }}
                            >
                                Cancel
                            </button>
                        </div>
                    </form>
                )}

                <h2 className="title">Candidate List</h2>

                {error && !showForm && <div className="error">{error}</div>}

                {!error && candidates.length === 0 && !showForm && (
                    <div className="info">No candidates found.</div>
                )}

                {candidates.length > 0 && (
                    <table className="candidate-table">
                        <thead>
                            <tr>
                                <th>Username</th>
                                <th>CV</th>
                                <th>Position</th>
                                <th>Review</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {candidates.map((c) => (
                                <tr key={c.username}>
                                    <td>{c.username}</td>
                                    <td>
                                        {c.file_Cv ? (
                                            <button
                                                className="btn view-cv-btn"
                                                onClick={() => {
                                                    setCvUrl(`https://localhost:7159/CV/${c.file_Cv}`);
                                                    setShowCvModal(true);
                                                }}
                                            >
                                                View CV
                                            </button>
                                        ) : (
                                            <span>No file</span>
                                        )}
                                    </td>
                                    <td>{c.position}</td>
                                    <td>{c.review}</td>
                                    <td>
                                        <button onClick={() => handleEdit(c)}>Edit</button>
                                        <button onClick={() => handleDelete(c.username)}>Delete</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>

            {showCvModal && (
                <div className="pdf-modal-overlay" onClick={() => setShowCvModal(false)}>
                    <div className="pdf-modal" onClick={(e) => e.stopPropagation()}>
                        <iframe
                            src={cvUrl}
                            width="100%"
                            height="600px"
                            title="Candidate CV"
                            style={{ border: 'none' }}
                        ></iframe>
                        <button className="close-modal" onClick={() => setShowCvModal(false)}>Close</button>
                    </div>
                </div>
            )}
        </SidebarLayout>
    );
};

export default Candidates;
