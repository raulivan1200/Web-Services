import { Alert, Box, CircularProgress, Container, CssBaseline } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [loadError, setLoadError] = useState(false);

  useEffect(() => {
    const controller = new AbortController();

    const loadActivities = async () => {
      try {
        const response = await axios.get<Activity[]>('https://localhost:5001/api/v1/events', {
          signal: controller.signal,
        });
        setActivities(response.data);
      } catch (error) {
        if (!axios.isCancel(error)) setLoadError(true);
      } finally {
        if (!controller.signal.aborted) setIsLoading(false);
      }
    };

    void loadActivities();
    return () => controller.abort();
  }, []);

  const handleSelectActivity = (id: string) => {
    setSelectedActivity(activities.find(act => act.id === id));
    setEditMode(false);
  }

  const handleCancelSelectActivity = () => {
    setSelectedActivity(undefined);
  }

  const handleOpenForm = (id?: string) => {
    if (id)
      handleSelectActivity(id);
    else
      handleCancelSelectActivity();
    setEditMode(true);
  }

  const handleFormClose = () => {
    setEditMode(false);
  }

  const handleSubmitForm = (activity: Activity) => {
    if (activity.id) {
      setSelectedActivity(activity)
      setActivities(current => current.map(act => act.id === activity.id ? activity : act))
    }
    else {
      const newActivity = { ...activity, id: crypto.randomUUID() }
      setSelectedActivity(newActivity)
      setActivities(current => [...current, newActivity])
    }
    setEditMode(false);
  }

  const handleDeleteActivity = (id: string) => {
    setActivities(current => current.filter(act => act.id !== id));
    if (selectedActivity?.id === id) {
      handleCancelSelectActivity();
      setEditMode(false);
    }
  }

  return (
    <Box sx={{ bgcolor: '#eeeeee' }}>
      <CssBaseline />
      <NavBar openForm={handleOpenForm} disabled={isLoading} />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        {isLoading ? <CircularProgress aria-label="Loading activities" /> : (
          <>
            {loadError && <Alert severity="error">Activities could not be loaded. You can still create activities in this session.</Alert>}
            <ActivityDashboard
              activities={activities}
              selectActivity={handleSelectActivity}
              cancelSelectActivity={handleCancelSelectActivity}
              selectedActivity={selectedActivity}
              editMode={editMode}
              openForm={handleOpenForm}
              closeForm={handleFormClose}
              submitForm={handleSubmitForm}
              deleteActivity={handleDeleteActivity} />
          </>
        )}
      </Container>
    </Box>
  )
}

export default App
