import Cookies from "universal-cookie";


function Logout() {
 
    const cookies = new Cookies();
    cookies.remove("token");
    cookies.remove("email");
    cookies.remove("fullName");
    cookies.remove("profilePicture");
    cookies.remove("unreadReceived");
    cookies.remove("unreadSent");
    window.location.href = '/';
  
  
}

export default Logout;
