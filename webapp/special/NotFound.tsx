import * as React from 'react';

import { Link } from 'react-router';

export class NotFound extends React.Component<any, any> {
    render(): JSX.Element {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <div className="error-template" style={{padding: "40px 15px", textAlign: "center" }}>
                            <h1>
                                Oops!</h1>
                            <h2>
                                404 Not Found</h2>
                            <div className="error-details">
                                Sorry, an error has occured, Requested page not found!
                                </div>
                            <div className="error-actions" style={{marginTop:"15px", marginBottom: "15px" }}>
                                <Link to="/" className="btn btn-primary btn-lg" style={{ marginRight: "10px" }}><span className="glyphicon glyphicon-home"></span>
                                    Take Me Home </Link><Link to="/contact" className="btn btn-default btn-lg" style={{ marginRight: "10px" }}><span className="glyphicon glyphicon-envelope"></span> Contact Support </Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        );
    }
};

