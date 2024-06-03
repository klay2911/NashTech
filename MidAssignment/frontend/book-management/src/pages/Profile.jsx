import React, { useState, useEffect } from "react";
import { apiGetProfile } from "../services/profile.service";

const Profile = () => {
  const userId = localStorage.getItem("userId");
  const [profile, setProfile] = useState({});

  const fetchPost = async (id) => {
    try {
      const response = await apiGetProfile(id);
      setProfile(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchPost(userId);
  }, [userId]);
  
  return (
    <div className="card shadow-lg p-3 mb-5 bg-white rounded">
      <div className="card-body">
        {/* <h5 className="card-title text-primary">ID: {profile?.id}</h5> */}
        <h6 className="card-subtitle mb-2 text-muted">Name: {profile?.name}</h6>
        <h6 className="card-subtitle mb-2 text-muted">Name: {profile?.email}</h6>
        <h6 className="card-subtitle mb-2 text-muted">Name: {profile?.name}</h6>
        <h6 className="card-subtitle mb-2 text-muted">Name: {profile?.name}</h6>
        <h6 className="card-subtitle mb-2 text-muted">Name: {profile?.name}</h6>
      </div>
    </div>
  );
};

export default Profile;