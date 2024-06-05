import React, { useEffect, useState } from "react";
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
    <div className="card mb-5 rounded bg-white p-3 shadow-lg">
      <div className="card-body">
        {/* <h5 className="card-title text-primary">ID: {profile?.id}</h5> */}
        <h6 className="card-subtitle text-muted mb-2">Name: {profile?.name}</h6>
        <h6 className="card-subtitle text-muted mb-2">
          Name: {profile?.email}
        </h6>
        <h6 className="card-subtitle text-muted mb-2">Name: {profile?.name}</h6>
        <h6 className="card-subtitle text-muted mb-2">Name: {profile?.name}</h6>
        <h6 className="card-subtitle text-muted mb-2">Name: {profile?.name}</h6>
      </div>
    </div>
  );
};

export default Profile;
